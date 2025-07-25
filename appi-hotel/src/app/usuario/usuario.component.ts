import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { User } from '../Interfaces/user';
import { UserService } from '../Service/user.service';
import { finalize } from 'rxjs';
import { Person } from '../Interfaces/person';
import { PersonService } from '../Service/person.service';
import { AuthServiceService } from '../auth-service.service';

@Component({
  selector: 'app-usuario',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './usuario.component.html',
  styleUrl: './usuario.component.css'
})
export class UsuarioComponent {
  
  usuarios: User [] = [];
  personaList: Person [] = [];

  usersForm!: FormGroup;
  showForm: boolean = false;
  editIndex: number | null = null;
  isLoading: boolean = false;
    esAdmin: boolean = false;
  
    constructor(private usersService: UserService, private personServices: PersonService, private fb: FormBuilder, private authService: AuthServiceService ) {}
  
   
    ngOnInit(): void {
      this.loadCustomer();
      this.loadTypeDocument();
      this.usersForm = this.fb.group({
        id: [null], // <- esto está bien
        username: ['', Validators.required],
        email: ['', Validators.required],
        password: ['', Validators.required],
        active: [false],
        personid: [null, Validators.required],
      });
       this.esAdmin = this.authService.isAdmin();
    }

    loadCustomer(): void {
        this.isLoading = true;
        this.usersService.getRooms()
          .pipe(finalize(() => this.isLoading = false))
          .subscribe({
            next: (data) => {
              console.log('Usuarios cargados:', data);
              this.usuarios = [...data];
            },
            error: (error) => {
              console.error('Error al cargar lo usuarios:', error);
            }
          });
      }


      loadTypeDocument(): void {
          this.personServices.getAll().subscribe({
            next: (data) => {
              this.personaList = data;
            },
            error: (err) => console.error('Error al cargar las personas:', err)
          });
        }
      
        refrescar(): void {
          this.loadCustomer();
        }
      
        toggleForm(): void {
          this.showForm = !this.showForm;
          this.usersForm.reset();
          this.editIndex = null;
        }
      
        closeForm(): void {
          this.showForm = false;
          this.usersForm.reset();
          this.editIndex = null;
        }
      
        trackByHotelId(index: number, hotel: User): number {
          return hotel.id;
        }

        submitForm(): void {
          if (this.usersForm.valid) {
            const { id, ...newUser } = this.usersForm.value; // <- eliminamos `id`
        
            this.isLoading = true;
            this.usersService.addRoom(newUser)
              .pipe(finalize(() => this.isLoading = false))
              .subscribe({
                next: () => {
                  this.loadCustomer();
                  this.closeForm();
                },
                error: (error) => {
                  console.error('Error al agregar el usuario:', error);
                }
              });
          }
        }
          editHotel(index: number): void {
            const selectedUser = this.usuarios[index];
            this.usersForm.patchValue({
              id: selectedUser.id,
              username: selectedUser.username,
              email: selectedUser.email,
              password: selectedUser.password,
              active: selectedUser.active,
              personid: selectedUser.personid
            });
            this.showForm = true;
            this.editIndex = index;
          }

          updateHotel(): void {
            if (this.usersForm.valid) {
              const formData = this.usersForm.value;
          
              this.isLoading = true;
          
              this.usersService.update(formData.id, formData)
                .pipe(finalize(() => this.isLoading = false))
                .subscribe({
                  next: () => {
                    this.loadCustomer(); // recarga la lista
                    this.closeForm();    // cierra el formulario
                  },
                  error: (error) => {
                    console.error('Error al actualizar el usuario:', error);
                  }
                });
            }
          }

          deleteForm(id: number): void {
            if (confirm('¿Estás seguro de eliminar este usuario?')) {
              this.isLoading = true;
              this.usersService.deleteRoom(id)
                .pipe(finalize(() => this.isLoading = false))
                .subscribe({
                  next: () => {
                    this.loadCustomer(); // recargar la lista
                  },
                  error: (error) => {
                    console.error('Error al eliminar el usuario:', error);
                  }
                });
            }
          }

           deleteLogicHotel(id: number): void {
             const confirmado = window.confirm('¿Deseas recuperar este formulario eliminado?');
  
            if (!confirmado) return;
            this.isLoading = true;
          
            this.usersService.deleteLogic(id)
              .pipe(finalize(() => this.isLoading = false))
              .subscribe({
                next: () => {
                  this.loadCustomer(); 
                },
                error: (err) => console.error('Error al realizar eliminación lógica:', err)
              });
  }

   recuperarEliminados(id: number): void {
    const confirmado = window.confirm('¿Deseas recuperar este formulario eliminado?');
  
    if (!confirmado) return;
  
    this.isLoading = true;
  
    this.usersService.patchRestore(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.loadCustomer(); 
        },
        error: (err) => console.error('Error al restaurar el formulario:', err)
      });
  }


}
