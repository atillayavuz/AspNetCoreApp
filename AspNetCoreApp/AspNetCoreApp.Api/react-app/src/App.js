import React from "react";
import "./App.css";
import Main from "./components/Layout/Main";
import Navbar from "./components/Layout/Navbar";

const App = () => (
  <div>
    <Navbar />
    <div className="container">
      <Main />
    </div>
  </div>
);

export default App;
