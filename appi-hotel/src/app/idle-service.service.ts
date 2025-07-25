import { Injectable, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { decodeToken } from './decodeToken';

@Injectable({
  providedIn: 'root'
})
export class IdleServiceService {

  private logoutTimeout: any;
  private idleTimeMs: number = 0;
  private watching = false;
  private boundResetTimer = this.resetTimer.bind(this);

  constructor(private router: Router, private ngZone: NgZone) {}

  public startWatching() {
    if (this.watching) return;
    this.watching = true;

    const token = localStorage.getItem('token');
    if (!token) return;

    const decoded = decodeToken(token);
    const idleMinutes = parseInt(decoded?.inactividad);
    if (isNaN(idleMinutes) || idleMinutes <= 0) return;

    this.idleTimeMs = idleMinutes * 60 * 1000;

    this.ngZone.runOutsideAngular(() => {
      ['click', 'mousemove', 'keypress', 'touchstart'].forEach(event =>
        document.addEventListener(event, this.boundResetTimer)
      );
    });

    this.setTimers();
  }

  private setTimers() {
    this.clearTimers();

 
    this.logoutTimeout = setTimeout(() => {
      this.logout();
    }, this.idleTimeMs);
  }

  private resetTimer() {
    this.setTimers(); // reinicia el conteo cada vez que hay interacción
  }

  private clearTimers() {
    clearTimeout(this.logoutTimeout);
  }

  private logout() {
    this.clearTimers();
    this.watching = false;
    localStorage.removeItem('token');
    alert('Tu sesión ha caducado por inactividad');
    this.router.navigate(['/login']);
  }

  public stopWatching() {
    this.clearTimers();
    this.watching = false;
  
    ['click', 'mousemove', 'keypress', 'touchstart'].forEach(event =>
      document.removeEventListener(event, this.boundResetTimer)
    );
  }

}
