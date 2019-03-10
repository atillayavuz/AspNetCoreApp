import React, { Component } from "react";
import "./App.css";
import Layout from "./components/Layout/Layout";
import Todo from "./container/Todo";

class App extends Component {
  render() {
    return (
      <div>
        <Layout>
          <Todo />
        </Layout>
      </div>
    );
  }
}

export default App;
