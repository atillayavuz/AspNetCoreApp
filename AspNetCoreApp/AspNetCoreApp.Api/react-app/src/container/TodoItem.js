import React, { Component } from "react";
import { Link } from "react-router-dom";
import { MDBListGroupItem } from "mdbreact";

class TodoItem extends Component {
  constructor(props) {
    super(props);
    this.remove = this.remove.bind(this);
  }

  remove() {
    this.props.removeTodo(this.props.item);
  }

  render() {
    const { id, title, description } = this.props.item;
    return (
      <MDBListGroupItem>
        {title + " " + description + " "}
        <Link to={`/todo/${id}`}>
          {" "}
          <i className="far fa-check-circle" />
        </Link>{" "}
        <i onClick={this.remove} className="far fa-times-circle" />
      </MDBListGroupItem>
    );
  }
}

export default TodoItem;
