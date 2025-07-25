import { HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { AuthServiceService } from "./auth-service.service";

export const JwtInterceptor: HttpInterceptorFn = (req, next) => {
    const authService = inject(AuthServiceService);
    const token = authService.getToken();
    
    console.log("token generado en el interceptor", token);
  
    if (token) {
      const cloned = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
      return next(cloned);
    }
  
    return next(req);
  };
  