import ReactDOM, { render } from "react-dom";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import React from 'react';
import App from './App';
import Player from "./Player";

const rootElement = document.getElementById("root");
render(
  <BrowserRouter>
    <Routes>
      <Route path="/" element={<App />} />
      <Route path="player/:username" element={<Player />} />
    </Routes>
  </BrowserRouter>,
  rootElement
);

// export default function What() {
//   return (
//     <BrowserRouter>
//       <Routes>
//         <Route path="/" element={<App />} />
//         <Route path="/player" element={<Player />} />
//       </Routes>
//     </BrowserRouter>
//   )
// }

// ReactDOM.render(
//     <App />,
//   document.getElementById('root')
// ); 
