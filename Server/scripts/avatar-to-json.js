const fs = require("fs");

const csv = fs.readFileSync(__dirname + "/avatars.csv", { encoding: "utf-8" })

const rows = csv.split("\n")

const avatars = {
    meta: {
        capacity: 0
    },
    data: []
}

const { meta, data } = avatars

rows.forEach(function each(row) {
    const column = row.split(",")
    const id = column[0]
    const name = column[1].replace(/"|\r/g, '')

    const obj = {
        id,
        "type": "avatar",
        "attributes": {
            name
        }
    }

    data.push(obj)
})

meta.capacity = data.length

fs.writeFile(__dirname + "/../data/avatars.json", JSON.stringify(avatars), function complete() {
    console.log("Your avatars.json is completed!");
})




