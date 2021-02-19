const WebSocket = require('ws')

const server = new WebSocket.Server({ port: 8080 })

let rooms = new Map()

server.on('connection', function connect(myself) {
    console.log(server.clients.size);
    myself["my_room"] = ""

    myself.on("message", function receive(json) {
        const obj = JSON.stringify(json)
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
            case "leaveRoom":
                if (rooms.has(obj.data)) {
                    rooms.get(obj.data).clients.delete(myself)
                    myself.send(JSON.stringify({
                        eventName: "leaveRoom",
                        data: "200"
                    }))
                } else {
                    myself.send(JSON.stringify({
                        eventName: "leaveRoom",
                        data: "400"
                    }))
                }
                break;
            case "sendMesage":
                const [roomName, message] = obj.data.split("|")
                if (rooms.has(roomName)) {
                    rooms.get(roomName).clients.forEach(function each(client) {
                        if (client !== myself && client.readyState === WebSocket.OPEN) {
                            myself.send(JSON.stringify({
                                eventName: "sendMessage",
                                data: message
                            }))
                        }
                    })
                } else {
                    myself.send(JSON.stringify({
                        eventName: "sendMessage",
                        data: "400"
                    }))
                }
                break;
        }
    })

    myself.on("close", function error(code, reason) {
        console.log("error: " + code)
        console.log("reason: " + reason)
        console.log(server.clients.size);

        let my_room = ""
        if (hasOwnProperty("my_room"))
            my_room = myself["my_room"]

        console.log(`room name: ${my_room}, current client no. : ${rooms.get(my_room).clients.size} `)

        if (my_room !== "" && rooms.has(my_room)) {
            rooms.get(my_room).clients.delete(myself)
            console.log(`room name: ${my_room}, after exit client no. : ${rooms.get(my_room).clients.size} `)
        }
    })
});

