const express = require("express");
const bodyParser = require("body-parser");
const cors = require("cors");
const app = express();
const mysql = require('mysql');

const PORT = 3001;

const db = mysql.createPool({
    host: "us-cdbr-east-04.cleardb.com",
    user: "b30684789398e6",
    password: "e350e762",
    database: "heroku_beb4b0dcad2a2a5"
});

app.use(cors());
app.use(express.json());
app.use(bodyParser.urlencoded({extended: true}));

app.get("/api/get", (req, res) => {
    const sql_select_all = "SELECT * FROM game_data ORDER BY score DESC, shot_accuracy DESC, balloons_popped DESC, boosts_used ASC, username ASC";
    db.query(sql_select_all, (err, result) => {
        res.send(result);
    })
});

app.post("/api/user", (req, res) => {
    const username = req.body.username;
    const sql_select_user = "SELECT * FROM game_data WHERE username = " + '"' + username + '" ORDER BY score DESC, shot_accuracy DESC, balloons_popped DESC, boosts_used ASC, username ASC'
    //const trivial_sql_insert = "INSERT INTO game_data VALUES (7,?,0,0,0,0)";
    db.query(sql_select_user, (err, result) => {
        res.send(result);
    });
});

app.listen(process.env.PORT || PORT, () => {
    console.log(`running on port ${PORT}`);
});