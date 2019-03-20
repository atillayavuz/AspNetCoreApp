import React, { Component } from "react";
import { SERVICE_BASE } from "../constants/config";

class TodoDetails extends Component {
  constructor(props) {
    super(props);
    this.state = {
      details: ""
    };
  }

  componentDidMount() {
    this.getTodo();
  }

  getTodo() {
    let taskId = this.props.match.params.id;

    fetch(`${SERVICE_BASE}api/Task/${taskId}`)
      .then(response => response.json())
      .then(details => this.setState({ details }));
  }

  render() {
    return (
      <div>
        {this.state.details.title}
        <h1>Details</h1>
      </div>
    );
  }
}

export default TodoDetails;
