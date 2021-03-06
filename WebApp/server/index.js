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
    const sql_select_all = "SELECT * FROM game_data ORDER BY score DESC, shot_accuracy DESC, balloons_popped DESC, boosts_used DESC, username ASC";
    db.query(sql_select_all, (err, result) => {
        res.send(result);
    })
});

app.post("/api/user", (req, res) => {
    const username = req.body.username;
    const sql_select_user = "SELECT * FROM game_data WHERE username = " + '"' + username + '" ORDER BY score DESC, shot_accuracy DESC, balloons_popped DESC, boosts_used DESC, username ASC'
    //const trivial_sql_insert = "INSERT INTO game_data VALUES (7,?,0,0,0,0)";
    db.query(sql_select_user, (err, result) => {
        res.send(result);
    });
});

app.post("/api/insert", (req, res) => {
    const username = req.body.username;
    const score = req.body.score;
    const balloons_popped = req.body.balloons_popped;
    const shot_accuracy = req.body.shot_accuracy;
    const boosts_used = req.body.boosts_used;
    const game_map = req.body.game_map;
    const control = req.body.control;
    const sql_insert_user = "INSERT INTO game_data VALUES (NULL,?,?,?,?,?,?,?)";
    db.query(sql_insert_user, [username, score, balloons_popped, shot_accuracy, boosts_used, game_map, control], (err,result) => {
        res.send(result);
    })
});

app.post("/api/player/data", (req, res) => {
    const username = req.body.username;
    const sql_select_player_data = "SELECT * FROM user_data WHERE username = " + '"' + username + '"';
    db.query(sql_select_player_data, (err, result) => {
        res.send(result);
    });
});

app.post("/api/player/insert", (req, res) => {
    const username = req.body.username;
    const hash = req.body.hash;
    const salt = req.body.salt;
    const sql_insert_player = "INSERT INTO user_data VALUES (?, ?, ?, 100, 100, 1, 0, \"sunset\")";
    db.query(sql_insert_player, [username, hash, salt], (err,result) => {
        res.send(result);
    });
});

app.post("/api/player/settings", (req, res) => {
    const username = req.body.username;
    const music_volume = req.body.music_volume;
    const engine_volume = req.body.engine_volume;
    const minimap = req.body.minimap;
    const retro_camera = req.body.retro_camera;
    const daytime = req.body.daytime;
    const sql_insert_settings = "UPDATE user_data SET music_volume=?, engine_volume=?, minimap=?, retro_camera=?, daytime=? WHERE username=?";
    db.query(sql_insert_settings, [music_volume, engine_volume, minimap, retro_camera, daytime, username], (err, result) => {
        res.send(result);
    });
});

app.get("/api/achievements", (req, res) => {
    const sql_get_achievements = "SELECT * FROM achievements";
    db.query(sql_get_achievements, (err, result) => {
        res.send(result);
    });
});

app.post("/api/achievements/new", (req, res) => {
    const username = req.body.username;
    const sql_new_achievement_entry = "INSERT INTO achievement_data (username) VALUES (?)";
    db.query(sql_new_achievement_entry, [username], (err, result) => {
        res.send(result);
    });
});

app.post("/api/achievements/get", (req, res) => {
    const username = req.body.username;
    const sql_get_user_achievements = "SELECT * FROM achievement_data WHERE username=?";
    db.query(sql_get_user_achievements, [username], (err, result) => {
        res.send(result);
    });
});

app.post("/api/achievements/update", (req,res) => {
    const username = req.body.username;
    const id = req.body.id;
    const value = req.body.value;
    const sql_update_user_achievements = "UPDATE achievement_data SET id" + id + "=" + value + " WHERE username=?";
    db.query(sql_update_user_achievements, [username], (err, result) => {
        res.send(result);
    });
});

app.get("/MacOS", (req, res) => {
    res.redirect("https://drive.google.com/uc?id=10SRmmwc2BfGoA-bBRE0sfiGRCw6iPv-7&export=download");
});

app.listen(process.env.PORT || PORT, () => {
    console.log(`running on port ${PORT}`);
});