import React, { Component } from "react";
import logo from "./logo.svg";
import "./App.css";

class App extends Component {
  constructor(props) {
    super(props);
    this.handleChangeTitle = this.handleChangeTitle.bind(this);
    this.handleChangeDescription = this.handleChangeDescription.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.state = {
      title: "",
      description: "",
      baseUrl: "http://localhost:2684/",
      tasks: []
    };
  }
  componentDidMount() {
    this.getTasks();
  }

  getTasks() {
    const url = this.state.baseUrl + "api/Task";
    fetch(url)
      .then(response => response.json())
      .then(tasks => this.setState({ tasks }));
  }

  handleChangeTitle(e) {
    this.setState({ title: e.target.value });
  }

  handleChangeDescription(e) {
    this.setState({ description: e.target.value });
  }

  handleSubmit(e) {
    e.preventDefault();
    const url = this.state.baseUrl + "api/Task";
    const { title, description } = this.state;

    fetch("http://localhost:2684/api/Task", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({ Title: title, Description: description })
    })
      .then(task => {
        debugger;
        if (task.status == 200) {
          this.setState({ title: "", description: "" });
          this.getTasks();
        }
      })
      .catch(err => console.log);
  }

  render() {
    const { tasks, title, description } = this.state;
    return (
      <div className="App">
        <form onSubmit={this.handleSubmit}>
          <div className={"form-group" + (!title ? " has-error" : "")}>
            <label htmlFor="title" className="">
              Title
            </label>
            <input
              type="text"
              className="form-control"
              name="title"
              value={title}
              onChange={this.handleChangeTitle}
            />
          </div>
          <div className={"form-group" + (!description ? " has-error" : "")}>
            <label htmlFor="description">Description</label>
            <input
              type="text"
              className="form-control"
              name="description"
              value={description}
              onChange={this.handleChangeDescription}
            />
            <div className="form-group">
              <button className="btn btn-primary">Kaydet</button>
            </div>
          </div>
        </form>
        <p>
          {tasks.map(task => {
            return (
              <span key={task.id}>{task.title + " " + task.description}</span>
            );
          })}
        </p>
      </div>
    );
  }
}

export default App;
