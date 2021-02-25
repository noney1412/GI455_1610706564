const sqlite3 = require('sqlite3').verbose();
const db = new sqlite3.Database("./database/User.db");

const [username, password] = "CN1wrwerwe234dfsdfsdf|1234".split('|');

db.get(`SELECT Username,Password,Name FROM UserTable WHERE (Username=$username and Password=$password)`, {
    $username: username,
    $password: password
}, function result(err, user) {
    if (err || user == undefined) {
        console.log(err)
    }

    if (user !== undefined)
        console.log(user)
})