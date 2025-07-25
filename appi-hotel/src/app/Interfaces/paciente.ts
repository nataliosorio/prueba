export interface paciente {
    Id: number;
  NombreCompleto: string;
  FechaNacimiento: string; // Se usa string porque Angular maneja fechas en formato ISO
  Genero: string;
  Telefono: string;
  Direccion: string;
}