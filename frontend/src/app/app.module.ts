import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AccordionModule } from 'primeng/accordion';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RaportComponent } from './raport/raport.component';
import { SideMenuComponent } from './side-menu/side-menu.component';
import { TopMenuComponent } from './top-menu/top-menu.component';
import { MenubarModule } from 'primeng/menubar';

const primeng = [AccordionModule];
@NgModule({
  declarations: [
    AppComponent,
    TopMenuComponent,
    SideMenuComponent,
    RaportComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MenubarModule,
    ...primeng,
  ],
  providers: [],
  bootstrap: [
    AppComponent,
    TopMenuComponent,
    SideMenuComponent,
    RaportComponent,
  ],
})
export class AppModule {}
