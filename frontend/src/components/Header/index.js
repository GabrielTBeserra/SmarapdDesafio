import React, { Component } from "react";
import { Link } from "react-router-dom";
import "./style.css";

export class Header extends Component {
  render() {
    return (
      <div className="MenuBar">
        <Link to={`/`} className="link">
          Lista de salas
        </Link>
      </div>
    );
  }
}
