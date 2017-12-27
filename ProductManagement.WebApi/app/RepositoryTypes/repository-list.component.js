System.register(['angular2/core', 'angular2/router', './repository.service'], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, router_1, repository_service_1;
    var RepositoryListComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (repository_service_1_1) {
                repository_service_1 = repository_service_1_1;
            }],
        execute: function() {
            RepositoryListComponent = (function () {
                function RepositoryListComponent(_repositoryService) {
                    this._repositoryService = _repositoryService;
                    this.pageTitle = 'Repository List';
                }
                RepositoryListComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    console.log('In OnInit');
                    this._repositoryService.getRepositories()
                        .subscribe(function (repositories) { return _this.repositories = repositories; }, function (error) { return _this.errorMessage = error; });
                };
                RepositoryListComponent = __decorate([
                    core_1.Component({
                        selector: 'pm-repositories',
                        templateUrl: 'app/Repositories/repository-list.component.html',
                        directives: [router_1.ROUTER_DIRECTIVES]
                    }), 
                    __metadata('design:paramtypes', [repository_service_1.RepositoryService])
                ], RepositoryListComponent);
                return RepositoryListComponent;
            }());
            exports_1("RepositoryListComponent", RepositoryListComponent);
        }
    }
});
//# sourceMappingURL=repository-list.component.js.map