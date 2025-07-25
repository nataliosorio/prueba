export interface Citas {
    Id: number;
    FechaHora: Date;
    MotivoConsulta: string;
    PacienteId: number;
    NombrePaciente: string;
    DoctorId: number;
    NombreDoctor: string;
    active: boolean;
}