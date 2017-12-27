/**
 * System configuration for Angular 2 apps
 * Adjust as necessary for your application needs.
 */
(function (global) {

    // map tells the System loader where to look for things
    var map = {
        'app': 'app', // 'dist',
        '@angular': 'node_modules/@angular',
        'angular2-in-memory-web-api': 'node_modules/angular2-in-memory-web-api',
        'jquery': 'node_modules/jquery/',
        'lodash': 'node_modules/lodash/lodash.js',
        'moment': 'node_modules/moment/',
        'ng2-bootstrap': 'node_modules/ng2-bootstrap',
        'ng2-datetime': 'node_modules/ng2-datetime/',
        'ng2-slim-loading-bar': 'node_modules/ng2-slim-loading-bar',
        'ng2-bs3-modal': 'node_modules/ng2-bs3-modal',
        'rxjs': 'node_modules/rxjs',
        'symbol-observable': 'node_modules/symbol-observable',
        
        "angular2-jwt": "node_modules/angular2-jwt/angular2-jwt.js",
        "ng2-charts":"node_modules/ng2-charts/ng2-charts.js"
    };

    // packages tells the System loader how to load when no filename and/or no extension
    var packages = {
        'app': { main: 'main.js', defaultExtension: 'js' },
        'rxjs': { defaultExtension: 'js' },
        'angular2-in-memory-web-api': { main: 'index.js', defaultExtension: 'js' },
        'moment': { main: 'moment.js', defaultExtension: 'js' },
        'ng2-bootstrap': { main: 'ng2-bootstrap.js', defaultExtension: 'js' },
        'ng2-datetime': { main: 'index.js', defaultExtension: 'js' },
        'ng2-slim-loading-bar': { defaultExtension: 'js' },
        'ng2-bs3-modal': { defaultExtension: 'js' },
        'symbol-observable': { main: 'index.js', defaultExtension: 'js' },
        'angular2-jwt': { defaultExtention: 'js' },
        'ng2-charts':{defaultExtention:'js'}
    };

    var ngPackageNames = [
      'common',
      'compiler',
      'core',
      'http',
      'platform-browser',
      'platform-browser-dynamic',
      'router', // doesn't come with umd bundles ... for now
      'router-deprecated',
      'upgrade',
      'forms'
    ];

    // Individual files (~300 requests):
    function packIndex(pkgName) {
        packages['@angular/' + pkgName] = { main: 'index.js', defaultExtension: 'js' };
    }

    // Bundled (~40 requests):
    function packUmd(pkgName) {
        packages['@angular/' + pkgName] = { main: '/bundles/' + pkgName + '.umd.js', defaultExtension: 'js' };
    };

    // Most environments should use UMD; some (Karma) need the individual index files
    var setPackageConfig = System.packageWithIndex ? packIndex : packUmd;

    // Add package entries for angular packages
    ngPackageNames.forEach(setPackageConfig);

    // No umd for router yet
    //packages['@angular/router'] = { main: 'index.js', defaultExtension: 'js' };


    var config = {
        map: map,
        packages: packages
    }

    // filterSystemConfig - index.html's chance to modify config before we register it.
    if (global.filterSystemConfig) { global.filterSystemConfig(config); }

    System.config(config);

})(this);