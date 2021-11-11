import React, { useState, useEffect } from "react";
import './App.css';
import Axios from "axios";

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

function App() {
  let globalMode = true;
;  const [gameDataList, setGameDataList] = useState([]);
  const [userSearch, setuserSearch] = useState("");
  const [userData, setUserData] = useState([]);

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

  return (
    <div className="App">
      <div className="aeroplayHeader">
        <h1 onClick={updateScores}>Aeroplay</h1>
      </div>
      <div className="searchPlayer">
        <label>Search Player:</label>
        <input type="text" name="userSearch" placeholder="Player Name" onChange={(search) => {
          setuserSearch(search.target.value)
        }} onSubmit={submitUser}></input>
        <button onClick={submitUser}><img src="https://img.icons8.com/ios-glyphs/90/000000/search--v2.png"/></button>
        <button onClick={updateScores}><img src="https://img.icons8.com/material-rounded/96/000000/globe--v1.png"/></button>
      </div>
      <div className="dataSection">
        <div className="globalData">
          {global ? <h3>Global Statistics</h3> : <h3>Player Statistics</h3>}
          {getGlobalStats(global ? gameDataList : userData).map((value) => {
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
            gameDataList.slice(0,15).map((value) => {
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
            userData.slice(0,15).map((value) => {
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
