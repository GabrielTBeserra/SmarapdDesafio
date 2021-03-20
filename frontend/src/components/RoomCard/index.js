import React, { Component } from "react";
import "./style.css";

export class Card extends Component {
  render() {
    return (
      <div className="card">
        <button>{this.props.roomId}</button>
      </div>
    );
  }
}
