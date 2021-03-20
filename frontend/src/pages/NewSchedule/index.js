import React, { Component } from "react";
import "react-date-range/dist/styles.css";
import "react-date-range/dist/theme/default.css";
import "./style.css";

export class NewSchedule extends Component {
  constructor(props) {
    super(props);
    this.state = {
      id: this.props.match.params.id,
      titulo: "",
      horaInicial: new Date(),
      horaFinal: new Date(),
      dataInicial: new Date(),
      dataFinal: new Date(),
      mensagem: "",
    };

    this.handleFianlHour = this.handleFianlHour.bind(this);
    this.handleFinalDate = this.handleFinalDate.bind(this);
    this.handleInitialDate = this.handleInitialDate.bind(this);
    this.handleInitialHour = this.handleInitialHour.bind(this);
    this.handleTitle = this.handleTitle.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  componentDidMount() {
    this.LoadRoom();
  }

  LoadRoom() {
    fetch(`http://localhost:5001/salas/get?id=${this.state.id}`)
      .then((resp) => resp.json())
      .then((response) => {
        this.setState({ sala: response });
        this.CreateScheduleRange();
      });
  }

  CreateScheduleRange() {
    let scheduligns = this.state.sala.schedulings.map((asd) => {
      return {
        startDate: new Date(asd.startTime),
        endDate: new Date(asd.endTime),
        key: "selection",
      };
    });

    this.setState({ ranges: scheduligns });
  }

  handleInitialDate(event) {
    this.setState({ dataInicial: event.target.value });
  }

  handleInitialHour(event) {
    this.setState({ horaInicial: event.target.value });
  }

  handleFianlHour(event) {
    this.setState({ horaFinal: event.target.value });
  }

  handleFinalDate(event) {
    this.setState({ dataFinal: event.target.value });
  }

  handleTitle(event) {
    this.setState({ titulo: event.target.value });
  }

  handleSubmit(event) {
    let dataInicial = new Date(this.state.dataInicial);
    let splitHoraInicial = this.state.horaInicial.toString().split(":");
    dataInicial.setHours(splitHoraInicial[0], splitHoraInicial[1]);

    let dataFinal = new Date(this.state.dataFinal);
    let splitHoraFinal = this.state.horaFinal.toString().split(":");
    dataFinal.setHours(splitHoraFinal[0], splitHoraFinal[1]);

    fetch("http://localhost:5001/agendamento/insert", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        roomId: parseInt(this.state.id),
        title: this.state.titulo,
        startTime: dataInicial.toISOString(),
        endTime: dataFinal.toISOString(),
      }),
    })
      .then((asd) => asd.json())
      .then((resp) => {
        console.log(resp);

        this.setState({ mensagem: resp.message });
        if (resp.type === "SUCESS") {
          this.props.history.push(`/sala/${this.state.id}`);
        }
      });

    event.preventDefault();
  }

  render() {
    return (
      <div className="container">
        <form onSubmit={this.handleSubmit} className="form">
          <label>Titulo:</label>
          <input
            type="text"
            value={this.state.titulo}
            onChange={this.handleTitle}
          />

          <label>Data Inicial:</label>
          <input
            type="date"
            value={this.state.dataInicial}
            onChange={this.handleInitialDate}
          />
          <label>Hora Inicial:</label>
          <input
            type="time"
            value={this.state.horaInicial}
            onChange={this.handleInitialHour}
          />

          <label>Data Final:</label>
          <input
            type="date"
            value={this.state.dataFinal}
            onChange={this.handleFinalDate}
          />
          <label>Hora Final:</label>
          <input
            type="time"
            value={this.state.horaFinal}
            onChange={this.handleFianlHour}
          />

          <input type="submit" value="Enviar" />
          <p>{this.state.mensagem}</p>
        </form>
      </div>
    );
  }
}