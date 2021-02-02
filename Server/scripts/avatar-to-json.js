const fs = require("fs");

const avatars = fs.readFileSync(__dirname + "/avatar.csv", { encoding: "utf-8" })

const rows = avatars.split("\n")

const schema = {
    meta: {
        capacity: 0
    },
    data: []
}

const { meta, data } = schema

rows.forEach(function split(row) {
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

fs.writeFile("../data/avatar.json", JSON.stringify(schema), function complete() {
    console.log("Your avatar.json is completed!");
})




