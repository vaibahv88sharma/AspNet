import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { HeaderComponent } from './components/header/header.component';
import { CourseApplicationComponent } from './components/form/course-application/course-application.component';
import { CardComponent } from './components/shared/card/card.component';
import { PaginationComponent } from './components/shared/pagination/pagination.component';
import { PaginationControlComponent } from './components/shared/pagination-control/pagination-control.component';
import { PersonalinfoComponent } from './components/form/personalinfo/personalinfo.component';
import { EmailControlsComponent } from './components/form/personalinfo/email-controls/email-controls.component';
import { HomeDataService } from './components/shared/services/home-data.service';
import { CommonMethods } from './components/shared/public/common-methods';
import { OtherPersonalinfoComponent } from './components/form/other-personalinfo/other-personalinfo.component';
import { ComponentMessageService } from './components/shared/services/component-message.service';
import { AppConfigurableSettings } from './components/shared/services/app-configurable.settings';
import { FilterPipe } from './components/shared/pipe/filter.pipe/filter.pipe';
import { ResidencyAboriginalComponent } from './components/form/res-aboriginal/res-aboriginal.component';
import { PreviousQualificationComponent } from './components/form/prev-qual/prev-qual.component';
import { CourseCampusComponent } from './components/form/course-campus/course-campus.component';
import { CustomFilterArrayPipe } from './components/shared/pipe/filter.pipe/filterarray.pipe';
import { UsiDetailsComponent } from './components/form/usi-details/usi-details.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        HeaderComponent,
        CardComponent,
        PaginationComponent,
        PaginationControlComponent,
        CourseApplicationComponent,
        PersonalinfoComponent,
        EmailControlsComponent,
        OtherPersonalinfoComponent,
        FilterPipe,
        ResidencyAboriginalComponent,
        PreviousQualificationComponent,
        CourseCampusComponent,
        CustomFilterArrayPipe,
        UsiDetailsComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [HomeDataService, CommonMethods, ComponentMessageService, AppConfigurableSettings],
})
export class AppModuleShared {
}
