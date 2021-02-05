const WebSocket = require('ws')
const { Validator } = require("jsonschema")

const server = new WebSocket.Server({ port: 8080 });

server.on('connection', function connect(me) {

    me.on("open", function ready() {
        console.log("Hello");

    })

    me.on("message", function receive(data) {
        console.log(data)
        console.log(server.clients.size);
    })

    // me.on("message", function receive(data) {
    //     const json = JSON.parse(data)

    //     switch (json.data.type) {
    //         case value:

    //             break;

    //         default:
    //             break;
    //     }

    //     server.clients.forEach((client) => {
    //         if (client !== me && client.readyState === WebSocket.OPEN) {
    //             client.send(JSON.stringify({
    //                 uid: data.uid,
    //                 name: data.name,
    //                 avatarName: data.avatarName,
    //                 message: data.message
    //             }));
    //         }
    //     });
    // });

    me.on("close", function error(code, reason) {
        console.log("error: " + code)
        console.log("reason: " + reason)
        console.log(server.clients.size);
    })
});

