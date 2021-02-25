const sqlite3 = require('sqlite3').verbose();
const db = new sqlite3.Database("./database/User.db");

const [user, pass] = "CN1wrwerwe234dfsdfsdf|1234".split('|');

db.get(`SELECT Username FROM UserTable WHERE (Username=$username and Password=$password)`, {
    $username: user,
    $password: pass
}, function result(err, row) {
    if (err) {
        console.log(err.message)
    }

    if (row !== undefined)
        console.log(row)
})