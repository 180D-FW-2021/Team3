import React, { useState, useEffect} from "react";
import { useNavigate, useParams } from "react-router-dom"; 
import Axios from "axios";

function importAll(r) {
    let images = {};
    r.keys().map((item, index) => { images[item.replace('./', '')] = r(item); });
    return images;
}
  
const images = importAll(require.context('./img', false, /\.(png|jpe?g|svg)$/));

function iconGrabber(id, gotten) {
    if (gotten == 1) {
        try {
            return images["id" + id + ".png"].default;
        }
        catch (error) {
            return images["placeholder.png"].default;
        }
    }
    else {
        return images["locked.png"].default;
    }
}

function getMapEntries(entries, selectMap) {
    let filtered = [];
    if (entries) {
      for (let item in entries) {
        if (entries[item]) {
          if (entries[item].game_map == selectMap) {
            filtered.push(entries[item]);
          }
        }
      }
    }
    return filtered;
  }

export default function Player() {
    const [userData, setUserData] = useState([]);
    const [mapSelect, setMapSelect] = useState(["Low-Poly"]);
    const [achievements, setAchievements] = useState([]);
    const [achievementProgress, setAchievementProgress] = useState([]);
    const { username } = useParams(); 

    const openWindow = (url) => {
        const newWindow = window.open(url, '_blank', 'noopener,noreferrer');
        if (newWindow) newWindow.opener = null;
    }

    useEffect(() => {
        Axios.post("https://aeroplay.herokuapp.com/api/user", {username: username}).then((response) => {
            console.log("User Request For: " + username);
            setUserData(response.data);
        });
    }, []);

    useEffect(() => {
        Axios.get("https://aeroplay.herokuapp.com/api/achievements").then((response) => {
            console.log("Got Achievements List");
            setAchievements(response.data);
        });
    }, []);

    useEffect(() => {
        Axios.post("https://aeroplay.herokuapp.com/api/achievements/get", {username: username}).then((response) => {
            console.log("Got Achievement Progress For: " + username);
            let progress = [];
            if (!response.data[0]) {
                setAchievementProgress(Array(achievements.length).fill(0));
            }
            else {
                for (const [key, value] of Object.entries(response.data[0]))
                {
                    if (key == "id25") {
                        progress.push(value == 3 ? 1 : 0);
                    }
                    else if (key == "id26") {
                        progress.push(value == 5 ? 1 : 0);
                    }
                    else if (key == "id27") {
                        progress.push(value == 10 ? 1 : 0);
                    }
                    else {
                        progress.push(value);
                    }
                }
                setAchievementProgress(progress);
            }
        });
    }, []);

    const navigate = useNavigate();
    const toGlobalPage = () => {
        navigate("/");
    }

    return (
        <div className="Player">
            <div className="homeButton">
                <button title="Country Roads" onClick={toGlobalPage}>Take Me Home<img src="https://img.icons8.com/material-rounded/96/000000/globe--v1.png"/></button>
            </div>
            <div className="welcome">
                <h1>Welcome, {username}!</h1>
            </div>
            <div className="aeroplayHeader">
                <h1 onClick={() => openWindow('https://www.youtube.com/watch?v=dQw4w9WgXcQ')}>Aeroplay</h1>
            </div>
            
            <div className="scores">
                <div>
                    <div>
                        <h1>Scores</h1>
                    </div>
                    <div className="mapSelectScores">
                        <label>Map:</label>
                        <select id="map_select" onChange={(selected) => setMapSelect(selected.target.value)}>
                            <option value="Low-Poly">Low-Poly</option>
                            <option value="Realistic">Realistic</option>
                        </select>
                    </div>
                    <hr></hr>
                </div>
                <div className = "playerScoreTable">
                    <table>
                    <tr>
                        <th>Score</th>
                        <th>Balloons Popped</th>
                        <th>Shot Accuracy</th>
                        <th>Boosts Used</th>
                        <th>Control Scheme</th>
                    </tr>
                    {getMapEntries(userData, mapSelect).map((value) => {
                        return (
                            <tr>
                                <td>{value.score}</td>
                                <td>{value.balloons_popped}</td>
                                <td>{value.shot_accuracy}</td>
                                <td>{value.boosts_used}</td>
                                <td>{value.control[0].toUpperCase() + value.control.substring(1)}</td>
                            </tr>
                        )
                    })}
                    </table>
                </div>
            </div>

            <div className="achievements">
                <div>
                    <div>
                        <h1>Achievements</h1>
                    </div>
                    <hr></hr>
                </div>
                <div className="achievementList">
                    {achievements.map((value) => {
                        return (
                            <div className="achievementEntry">
                                <img src={iconGrabber(value.id, achievementProgress[value.id + 1])} alt="image"></img>
                                <div className="achievementText">
                                    <h4>{achievementProgress[value.id + 1] == 1 ? value.name : "???"}</h4>
                                    <h4>{value.description}</h4>
                                </div>
                            </div>
                        )
                    })}
                </div>
            </div>
        </div>
    )
}