import React, { Component } from "react";
import axios from "axios";
import { SERVICE_BASE } from "../constants/config";
import { MDBContainer, MDBRow, MDBCol, MDBBtn, MDBListGroup } from "mdbreact";
import TodoItem from "./TodoItem";

class Todo extends Component {
  constructor(props) {
    super(props);
    this.handleChangeTitle = this.handleChangeTitle.bind(this);
    this.handleChangeDescription = this.handleChangeDescription.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.removeTodo = this.removeTodo.bind(this);
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
    axios.get(`${SERVICE_BASE}api/Task`).then(res => {
      const tasks = res.data;
      this.setState({ tasks: tasks });
    });
  }

  handleChangeTitle(e) {
    this.setState({ title: e.target.value });
  }

  removeTodo(e) {
    const self = this;
    axios.delete(`${SERVICE_BASE}api/Task/${e.id}`).then(res => {
      self.setState({
        tasks: self.state.tasks.filter(function(task) {
          return task !== e;
        })
      });
    });
  }

  handleChangeDescription(e) {
    this.setState({ description: e.target.value });
  }

  handleSubmit(e) {
    e.preventDefault();
    const { title, description } = this.state;
    let self = this;
    const data = { Title: title, Description: description };

    let params = new URLSearchParams();
    params.append("description", description);
    params.append("title", title);

    axios
      .post(`${SERVICE_BASE}api/Task`, params)
      .then(res => {
        debugger;
        self.setState({
          tasks: [...self.state.tasks, data],
          title: "",
          description: ""
        });
      })
      .catch(error => {
        debugger;
        console.log(error.response);
      });
    // let params = new URLSearchParams();
    // params.append("description", description);
    // params.append("title", title);
    // axios({
    //   method: "POST",
    //   url: `${SERVICE_BASE}api/Task`,
    //   headers: {
    //     "content-type": "application/json"
    //   },
    //   data: params
    // }).then(obj => {
    //   self.setState({
    //     tasks: [...self.state.tasks, data],
    //     title: "",
    //     description: ""
    //   });
    // });
  }

  render() {
    const { tasks, title, description } = this.state;
    return (
      <div className="App">
        <MDBContainer>
          <MDBRow>
            <MDBCol md="4">
              <form onSubmit={this.handleSubmit}>
                <p className="h5 text-center mb-4">Save Todo Item</p>
                <label htmlFor="title" className="grey-text">
                  Title
                </label>
                <input
                  type="text"
                  id="title"
                  className="form-control"
                  name="title"
                  value={title}
                  onChange={this.handleChangeTitle}
                />
                <br />
                <label htmlFor="description" className="grey-text">
                  Description
                </label>
                <input
                  type="text"
                  id="description"
                  className="form-control"
                  name="description"
                  value={description}
                  onChange={this.handleChangeDescription}
                />

                <div className="text-center mt-4">
                  <MDBBtn color="indigo" type="submit">
                    SAve
                  </MDBBtn>
                </div>
              </form>
            </MDBCol>
            <MDBCol md="2" />
            <MDBCol md="4">
              <MDBListGroup style={{ width: "22rem" }}>
                {tasks.map(task => {
                  return (
                    <TodoItem
                      key={task.id}
                      item={task}
                      removeTodo={this.removeTodo}
                    />
                  );
                })}
              </MDBListGroup>
            </MDBCol>
          </MDBRow>
        </MDBContainer>
      </div>
    );
  }
}
export default Todo;
