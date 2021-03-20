import React, { Component } from "react";
import "./style.css";

export class Schedule extends Component {
  render() {
    return <div className="schedule">{this.props.text}</div>;
  }
}
