import { useNavigate, Outlet, Link } from "react-router-dom";
import React, { useState, useEffect } from "react";
import './App.css';
import Player from "./Player.js";
import Axios from "axios";
import userGuide from "./UserGuide.pdf"

function getGlobalStats(data) {
  let total_score = 0;
  let total_balloons_popped = 0;
  let total_accuracy = 0;
  let total_boosts = 0;
  let max_score = 0;
  let max_balloons_popped = 0;
  let max_accuracy = 0;
  if (data.length != 0) {
    for (let item in data) {
      if (data[item]) {
        total_score += data[item].score;
        total_balloons_popped += data[item].balloons_popped;
        total_accuracy += data[item].shot_accuracy;
        total_boosts += data[item].boosts_used;
        if (data[item].score > max_score) {
          max_score = data[item].score; 
        }
        if (data[item].balloons_popped > max_balloons_popped) {
          max_balloons_popped = data[item].balloons_popped;
        }
        if (data[item].shot_accuracy > max_accuracy) {
          max_accuracy = data[item].shot_accuracy;
        }
      }
    }
    return [`Maximum Score: ${max_score}`,`Average Score: ${(total_score/data.length).toFixed(2)}`,`Maximum Balloons Popped: ${max_balloons_popped}`,`Total Balloons Popped: ${total_balloons_popped}`,`Average Balloons Popped: ${(total_balloons_popped/data.length).toFixed(2)}`,`Maximum Shot Accuracy: ${max_accuracy.toFixed(2)}`,`Average Shot Accuracy: ${(total_accuracy/data.length).toFixed(2)}`,`Total Boosts: ${total_boosts}`]
  }
  else {
    return ["Stats Unavailable"]
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

function getControlEntries(entries, selectControl) {
  let filtered = [];
  if (entries) {
    for (let item in entries) {
      if (entries[item]) {
        if (entries[item].control == selectControl) {
          filtered.push(entries[item]);
        }
      }
    }
  }
  return filtered;
}

function App() {
  let globalMode = true;
  const [gameDataList, setGameDataList] = useState([]);
  const [userSearch, setuserSearch] = useState("");
  const [usernameSearch, setUsernameSearch] = useState("");
  const [userData, setUserData] = useState([]);
  const [mapSelect, setMapSelect] = useState("Low-Poly");
  const [controlSelect, setControlSelect] = useState("plane");

  useEffect(() => {
    Axios.get("https://aeroplay.herokuapp.com/api/get").then((response) => {
      console.log("Get Request")
      setGameDataList(response.data);
    });
    global = true;
  }, []);

  const updateScores = () => {
    Axios.get("https://aeroplay.herokuapp.com/api/get").then((response) => {
      console.log("Get Request");
      setGameDataList(response.data);
    });
    global = true;
  }

  const submitUser = () => {
    Axios.post("https://aeroplay.herokuapp.com/api/user", {username: userSearch}).then((response) => {
      console.log("User Request For: " + userSearch);
      setUserData(response.data)
    });
    global = false;
  }

  const openWindow = (url) => {
    const newWindow = window.open(url, '_blank', 'noopener,noreferrer');
    if (newWindow) newWindow.opener = null;
  }


  const navigate = useNavigate();
  const toPlayerPage = () => {
    navigate("/player/" + usernameSearch);
  }

  return (
    <div className="App">
      <div className="aeroplayHeader">
        <h1 onClick={() => openWindow('https://www.youtube.com/watch?v=dQw4w9WgXcQ')}>Aeroplay</h1>
      </div>
      <div className="download">
        <button onClick={() => openWindow('https://drive.google.com/uc?id=10SRmmwc2BfGoA-bBRE0sfiGRCw6iPv-7&export=download')}>
          <img src="https://img.icons8.com/material-rounded/96/000000/download--v2.png"/>
          Download for MacOS
        </button>
        <button onClick={() => openWindow('https://drive.google.com/uc?id=1N4CrwjidAtC3ua01Y6W8nwcnznfyPOtg&export=download')}>
          <img src="https://img.icons8.com/material-rounded/96/000000/download--v2.png"/>
          Download for Windows
        </button>
        <a href={userGuide} target="_blank" rel="noopener noreferrer">
          <button>
            <img src="https://img.icons8.com/ios-glyphs/90/000000/user-manual.png"/>
            User Guide
          </button>
        </a>
      </div>
      <hr></hr>
      <div className="searchPlayer">
        <label>Search Player:</label>
        <form onSubmit={toPlayerPage}>
          <input type="text" value={usernameSearch} onChange={(search) => setUsernameSearch(search.target.value)}></input>
        </form>
        
        <div className="mapSelect">
          <label>Map:</label>
          <select id="map_select" onChange={(selected) => setMapSelect(selected.target.value)}>
            <option value="Low-Poly">Low-Poly</option>
            <option value="Realistic">Realistic</option>
          </select>
        </div>
        <div className="controlSelect">
          <label>Controller:</label>
          <select id="control_select" onChange={(selected) => setControlSelect(selected.target.value)}>
            <option value="plane">Plane</option>
            <option value="keyboard">Keyboard</option>
          </select>
        </div>
      </div>
      
      <div className="dataSection">
        <div className="globalData">
          {global ? <h3>Global Statistics</h3> : <h3>Player Statistics</h3>}
          {getGlobalStats(global ? getControlEntries(getMapEntries(gameDataList, mapSelect), controlSelect) : getControlEntries(getMapEntries(userData, mapSelect), controlSelect)).map((value) => {
            return (
              <table>
                <tr>
                  <td>{value}</td>
                </tr>
              </table>
            )
          })}
        </div>
        <div className="dataTable">
          <table>
            <tr>
                <th>Username</th>
                <th>Score</th>
                <th>Balloons Popped</th>
                <th>Shot Accuracy</th>
                <th>Boosts Used</th>
            </tr>
          {global ? 
            getControlEntries(getMapEntries(gameDataList, mapSelect), controlSelect).slice(0,15).map((value) => {
              return (
              <tr>
                <td>{value.username}</td>
                <td>{value.score}</td>
                <td>{value.balloons_popped}</td>
                <td>{value.shot_accuracy}</td>
                <td>{value.boosts_used}</td>
              </tr>
              )
            }) : 
            getControlEntries(getMapEntries(userData, mapSelect), controlSelect).slice(0,15).map((value) => {
              return (
                <tr>
                  <td>{value.username}</td>
                  <td>{value.score}</td>
                  <td>{value.balloons_popped}</td>
                  <td>{value.shot_accuracy}</td>
                  <td>{value.boosts_used}</td>
                </tr>
              )
            })
          }
          </table>
        </div>
      </div>
    </div>
  );
}

export default App;
