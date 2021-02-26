const WebSocket = require('ws')
const server = new WebSocket.Server({ port: 8080 })

const sqlite3 = require('sqlite3').verbose();
const db = new sqlite3.Database("./database/User.db");

let rooms = new Map()

server.on('connection', function connect(myself) {
    myself["my_room"] = ""

    myself.on("message", function receive(json) {
        const obj = JSON.parse(json)

        switch (obj.eventName) {
            case "createRoom":
                if (!rooms.has(obj.data)) {
                    rooms.set(obj.data, { clients: new Set([myself]) })
                    myself["my_room"] = obj.data
                    myself.send(JSON.stringify({
                        eventName: "createRoom",
                        data: "200"
                    }))
                } else {
                    myself.send(JSON.stringify({
                        eventName: "createRoom",
                        data: "400"
                    }))
                }
                break;

            case "joinRoom":
                if (rooms.has(obj.data)) {
                    rooms.get(obj.data).clients.add(myself)
                    myself["my_room"] = obj.data
                    myself.send(JSON.stringify({
                        eventName: "joinRoom",
                        data: "200"
                    }))
                } else {
                    myself.send(JSON.stringify({
                        eventName: "joinRoom",
                        data: "400"
                    }))
                }
                break;

            case "sendMessage":
                {
                    const [roomName, message] = obj.data.split("|")
                    if (rooms.has(roomName)) {
                        rooms.get(roomName).clients.forEach(function each(client) {
                            console.log(client == myself)
                            if (client != myself && client.readyState == WebSocket.OPEN) {
                                client.send(JSON.stringify({
                                    eventName: "sendMessage",
                                    data: message
                                }))
                            }
                        })
                    }
                }
                break;

            // Database
            case "register": {
                const [userId, password, userName] = obj.data.split('|');

                db.run(`INSERT INTO UserTable
                VALUES ( $username, $password, $name )`, {
                    $username: userId,
                    $password: password,
                    $name: userName
                }
                    , (err) => {
                        if (err) {
                            myself.send(JSON.stringify({
                                eventName: "register",
                                data: "400"
                            }))
                        }

                        myself.send(JSON.stringify({
                            eventName: "register",
                            data: "200"
                        }))
                    })
            }
                break;

            case "login": {
                const [userId, password] = obj.data.split('|');
                db.get(`SELECT Username FROM UserTable WHERE (Username=$username AND Password=$password)`, {
                    $username: userId,
                    $password: password
                }, function result(err, row) {
                    if (err || row == undefined) {
                        myself.send(JSON.stringify({
                            eventName: "login",
                            data: "400"
                        }))
                    }

                    if (row !== undefined)
                        myself.send(JSON.stringify({
                            eventName: "login",
                            data: "200"
                        }))
                })
            }
                break;
        }
    })

    myself.on("close", function error(code, reason) {
        console.log("error: " + code)
        console.log("reason: " + reason)
        console.log("clients" + server.clients.size);

        let my_room = ""
        if (Object.prototype.hasOwnProperty.call(myself, "my_room")) {
            my_room = myself["my_room"]
            if (my_room !== "" && rooms.has(my_room)) {
                console.log(`room name: ${my_room}, current client no. : ${rooms.get(my_room).clients.size} `)
                rooms.get(my_room).clients.delete(myself)
                console.log(`room name: ${my_room}, after exit client no. : ${rooms.get(my_room).clients.size} `)
                if (rooms.get(my_room).clients.size == 0) {
                    rooms.delete(my_room)
                }
            }
        }
    })
});

