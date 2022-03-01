import React, { useState, useEffect} from "react";
import { useNavigate, useParams } from "react-router-dom"; 
import Axios from "axios";

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
        </div>
    )
}