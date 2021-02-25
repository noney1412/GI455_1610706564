const rooms = new Map()

// rooms.set("Room39", [1, 2, 3, 4, 5, "myself"])

// console.log(rooms.get("Room39"));

rooms.set("Room39", new Set([1, 2, 3, 4, 5, "myself"]))

console.log(rooms.get("Room39").has("myself"))

rooms.get("Room39").add("other")

console.log(rooms.get("Room39"));

rooms.get("Room39").forEach(client => {
    if (client != "myself") {
        console.log("hello");
    }
})