import React, { Component } from "react";
import logo from "./logo.svg";
import "./App.css";

class App extends Component {
  state = {
    baseUrl: "http://localhost:2684/",
    tasks: []
  };

  componentDidMount() {
    this.fetchData();
  }

  fetchData() {
    const url = this.state.baseUrl + "api/Task";
    fetch(url)
      .then(response => response.json())
      .then(tasks => this.setState({ tasks }));
  }

  render() {
    const { tasks } = this.state;
    return (
      <div className="App">
        <header className="App-header">
          <p>
            {tasks.map(task => {
              return <span key={task.id}>{task.title}</span>;
            })}
          </p>
        </header>
      </div>
    );
  }
}

export default App;
