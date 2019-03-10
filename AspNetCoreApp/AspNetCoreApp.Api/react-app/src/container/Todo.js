import React, { Component } from "react";
import { SERVICE_BASE } from "../constants/config";
import Aux from "../hoc/aux";

class Todo extends Component {
  constructor(props) {
    super(props);
    this.handleChangeTitle = this.handleChangeTitle.bind(this);
    this.handleChangeDescription = this.handleChangeDescription.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.state = {
      title: "",
      description: "",
      tasks: []
    };
    this.serviceBase = SERVICE_BASE;
  }
  componentDidMount() {
    this.getTasks();
  }

  getTasks() {
    fetch(`${SERVICE_BASE}api/Task`)
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
    const { title, description } = this.state;
    let self = this;

    fetch(`${SERVICE_BASE}api/Task/AddTask`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({ Title: title, Description: description })
    })
      .then(function(res) {
        debugger;
        return res.json();
      })
      .then(function(task) {
        debugger;
        self.setState({
          tasks: [...self.state.tasks, task],
          title: "",
          description: ""
        });
      })
      .catch(err => console.log);
  }

  render() {
    const { tasks, title, description } = this.state;
    return (
      <Aux>
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
      </Aux>
    );
  }
}

export default Todo;
