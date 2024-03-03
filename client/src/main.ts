import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';

// bootstrapApplication(AppComponent, appConfig)
//   .catch((err) => console.error(err));

setTimeout(() => {
  const loadingElement = document.querySelector(".parent-loader");

  bootstrapApplication(AppComponent, appConfig)
    .then(() => {
      console.log('Loaded');
      loadingElement?.classList.add("loader-hide")
    })
    // .then(() => {
    //   console.log('Set timeout function called');
    //   setTimeout(() => loadingElement?.remove(), 3000)
    // })
    .catch((err) => console.error(err));
}, 2000);
