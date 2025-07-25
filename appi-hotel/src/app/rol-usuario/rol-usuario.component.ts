import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RolUserService } from '../Service/rol-user.service';
import { UserService } from '../Service/user.service';
import { Rol } from '../Interfaces/rol';
import { User } from '../Interfaces/user';
import { RolUser } from '../Interfaces/rol-user';
import { finalize } from 'rxjs';
import { RolService } from '../Service/rol.service';
import { AuthServiceService } from '../auth-service.service';

@Component({
  selector: 'app-rol-usuario',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './rol-usuario.component.html',
  styleUrl: './rol-usuario.component.css'
})
export class RolUsuarioComponent {
 Rolusuarios: RolUser [] = [];
  RolList: Rol [] = [];
  UserList: User [] = [];


  RolUserForm!: FormGroup;
  showForm: boolean = false;
  editIndex: number | null = null;
  isLoading: boolean = false;
  esAdmin: boolean = false;

  
    constructor(private rolUserService: RolUserService, private personServices: UserService, private rolservice: RolService,private fb: FormBuilder, private authService: AuthServiceService ) {}
  
   
    ngOnInit(): void {
      this.loadRolUser();
      this.loadRol();
      this.loadUser();

      this.RolUserForm = this.fb.group({
        id: [null], 
        rolid: [null, Validators.required],
        userid: [null, Validators.required],
        active: [true],
      });
    this.esAdmin = this.authService.isAdmin();

    }

    loadRolUser(): void {
        this.isLoading = true;
        this.rolUserService.getRooms()
          .pipe(finalize(() => this.isLoading = false))
          .subscribe({
            next: (data) => {
              console.log('Usuarios cargados:', data);
              this.Rolusuarios = [...data];
            },
            error: (error) => {
              console.error('Error al cargar lo usuarios:', error);
            }
          });
      }


      loadUser(): void {
          this.personServices.getRooms().subscribe({
            next: (data) => {
              this.UserList = data;
            },
            error: (err) => console.error('Error al cargar las personas:', err)
          });
        }

        loadRol(): void {
          this.rolservice.getAll().subscribe({
            next: (data) => {
              this.RolList = data;
            },
            error: (err) => console.error('Error al cargar las personas:', err)
          });
        }
      
        refrescar(): void {
          this.loadRolUser();
        }
      
        toggleForm(): void {
          this.showForm = !this.showForm;
          this.RolUserForm.reset();
          this.editIndex = null;
        }
      
        closeForm(): void {
          this.showForm = false;
          this.RolUserForm.reset();
          this.editIndex = null;
        }
      
        trackByHotelId(index: number, hotel: RolUser): number {
          return hotel.id;
        }

        submitForm(): void {
          if (this.RolUserForm.valid) {
            const { id, ...newUser } = this.RolUserForm.value; // <- eliminamos `id`
        
            this.isLoading = true;
            this.rolUserService.addRoom(newUser)
              .pipe(finalize(() => this.isLoading = false))
              .subscribe({
                next: () => {
                  this.loadRolUser();
                  this.closeForm();
                },
                error: (error) => {
                  console.error('Error al agregar el Rolusuario:', error);
                }
              });
          }
        }
          editHotel(index: number): void {
            const selectedUser = this.Rolusuarios[index];
            this.RolUserForm.patchValue({
              id: selectedUser.id,
              rolid: selectedUser.rolid,
              userid: selectedUser.userid,
              active: selectedUser.active,
            });
            this.showForm = true;
            this.editIndex = index;
          }

          updateHotel(): void {
            if (this.RolUserForm.valid) {
              const formData = this.RolUserForm.value;
          
              this.isLoading = true;
          
              this.rolUserService.update(formData.id, formData)
                .pipe(finalize(() => this.isLoading = false))
                .subscribe({
                  next: () => {
                    this.loadRolUser(); // recarga la lista
                    this.closeForm();    // cierra el formulario
                  },
                  error: (error) => {
                    console.error('Error al actualizar el rolusuario:', error);
                  }
                });
            }
          }

          deleteHotel(id: number): void {
            if (confirm('¿Estás seguro de eliminar este Rol-usuario?')) {
              this.isLoading = true;
              this.rolUserService.deleteRoom(id)
                .pipe(finalize(() => this.isLoading = false))
                .subscribe({
                  next: () => {
                    this.loadRolUser(); // recargar la lista
                  },
                  error: (error) => {
                    console.error('Error al eliminar el rolusuario:', error);
                  }
                });
            }
          }

          deleteLogic(id: number): void {
    const confirmado = window.confirm('¿Estás seguro de que deseas eliminar  esta Relacion de Rol-Usuario?');
    if (!confirmado) return;
    this.isLoading = true;
  
    this.rolUserService.deleteLogic(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.loadRolUser(); 
        },
        error: (err) => console.error('Error al realizar eliminación lógica:', err)
      });
  }

   recuperarEliminados(id: number): void {
    const confirmado = window.confirm('¿Estás seguro de que deseas recuperar esta relación de Rol-Usuario eliminado?');
    if (!confirmado) return;
    this.isLoading = true;
  
    this.rolUserService.patchRestore(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.loadRolUser(); // Recarga la tabla para mostrar el registro restaurado
        },
        error: (err) => console.error('Error al restaurar el formulario:', err)
      });
  }
          

}
