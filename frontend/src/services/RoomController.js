export function GetRoomList() {
  fetch("http://localhost:5001/salas/listall")
    .then((resp) => resp.json())
    .then((response) => {
      return response;
    });
}
