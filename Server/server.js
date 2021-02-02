const WebSocket = require('ws')


const server = new WebSocket.Server({ port: 8080 });

server.on('connection', function connection(socket) {

    socket.on("open", function ready() {

    })

    socket.on("message", function incoming(data) {
        data = JSON.parse(data)
        console.log(data)

        // server.clients.forEach((client) => {
        //     if (client !== socket && client.readyState === WebSocket.OPEN) {
        //         client.send(JSON.stringify({
        //             uid: data.uid,
        //             name: data.name,
        //             displayImage: data.displayImage,
        //             message: data.message
        //         }));
        //     }
        // });
    });

    socket.on("close", function close(code, reason) {
        console.log(code)
        console.log(reason)
    })
});

