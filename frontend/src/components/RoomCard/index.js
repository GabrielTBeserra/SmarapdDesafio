import React, { Component } from "react";
import "./style.css";
import { Link } from "react-router-dom";
import { IoTrash } from "react-icons/io5";

export class Card extends Component {
  DeleteRoom(id) {
    if (window.confirm("Deseja realmente apagar essa sala?")) {
      fetch(`http://localhost:5001/salas/delete?id=${id}`, { method: "DELETE" })
        .then((resp) => resp.json())
        .then((response) => {
          window.location.reload();
        });
    }
  }

  render() {
    return (
      <div className="card">
        <Link to={`/sala/${this.props.roomId.roomId}`}>
          <button className="botao">Sala {this.props.roomId.roomId}</button>
        </Link>
        {this.props.roomId.schedulings === null && (
          <button
            className="botaodelete"
            onClick={() => this.DeleteRoom(this.props.roomId.roomId)}
          >
            <IoTrash />
          </button>
        )}
      </div>
    );
  }
}
