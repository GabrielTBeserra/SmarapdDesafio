import React, { Component } from "react";
import { Card } from "../../components/RoomCard";
import { Link } from "react-router-dom";
import "./style.css";

export class Main extends Component {
  constructor(props) {
    super(props);
    this.state = { salas: [] };
  }

  componentDidMount() {
    this.LoadRoomList();
  }

  LoadRoomList() {
    fetch("http://localhost:5001/salas/listall")
      .then((resp) => resp.json())
      .then((response) => {
        console.log(response);
        this.setState({ salas: response });
      });
  }

  render() {
    return (
      <div className="container">
        {this.state.salas.map((item) => (
          <Link to={`/sala/${item.roomId}`}>
            <Card roomId={item.roomId} as="teste" />
          </Link>
        ))}
      </div>
    );
  }
}
