import { Routes } from '@angular/router';

import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { DivisorPrimosComponent } from '../../divisor-primos/divisor-primos.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'user-profile',   component: UserProfileComponent },
    { path: 'divisor-primos',     component: DivisorPrimosComponent }
];
