var jsInterval;
var VERSION=new Date().getTime();
var SITE_NAME="ERP";
var API_URL='';

$.ajaxSetup({
    beforeSend: function(xhr, settings){
        var type='GET';
        if(typeof settings.method !== "undefined"){
            type=settings.method.toUpperCase();
        }else if(typeof settings.type !== "undefined"){
            type=settings.type.toUpperCase();
        }
        
        if(type=='POST'){
            //settings.data = addCsrfInData(settings.data);
            return true;
        }
    },
    dataFilter: function(res){
        if(typeof res == "string"){
            if(res.indexOf('class="login-bx"')!==-1){
                location.href=API_URL+'user/logout';
            }
        }

        return res;
    }
});

var App = angular.module('App', ['ui.bootstrap']);

function addTokenInData(data){
	var authToken=localStorage.getItem('AuthToken');
	if(angular.isString(data)){
		data=data+"&AuthToken="+authToken;
	}else if(angular.isObject(data)){
		if(angular.isFunction(data.append)){
			data.append('AuthToken', authToken);
		}else{
			data['AuthToken']=authToken;
		}
	}
	return data;
}

function addCsrfInData(data){
    Csrf_hash=get_csrf_cookie();
	if(angular.isString(data)){
        data=data.replace(Csrf_token, 'ethsjaj');
		data=data+"&"+Csrf_token+"="+Csrf_hash;
	}else if(angular.isObject(data)){
		if(angular.isFunction(data.append)){
			data.append(Csrf_token, Csrf_hash);
		}else{
			data[Csrf_token]=Csrf_hash;
		}
	}
	return data;
}

function paramSerializeData(data){
	if(angular.isObject(data)){
		if(!angular.isFunction(data.append)){
			data=$.param(data);
		}
	}
	return data;
}

function showHttpErr(res){
	alert("Network error! Try again.");
	//hide_loader();
}

/** Auth Factory **/
App.factory('Auth', function($window, $state){
	return{
		setSession: function(u){
			$window.localStorage['name']		=	angular.isDefined(u.name)?u.name:'';
			$window.localStorage['user_type']	=	angular.isDefined(u.type)?u.type:'';
		},
		getSession: function(){
			return $window.localStorage;
		},
		delSession: function(){
			$window.localStorage.removeItem('name');
			$window.localStorage.removeItem('user_type');
			$window.localStorage.removeItem('AuthToken');
		},
		setAuthToken: function(token){
			$window.localStorage['AuthToken']=token;
		},
		isLogged: function(){
			return (angular.isDefined($window.localStorage.name) && $window.localStorage.name!='');
		},
		sessionVal: function(key){
			var s=this.getSession();
			return angular.isDefined(s[key])?s[key]:'';
		}
	  }
});

App.factory('httpRequestInterceptor', function ($rootScope) {
    return {
        request: function (config) {
			if(config.url.indexOf(".html")===-1){
				if(config.method=='POST'){
					if(!angular.isDefined(config.data)){
						config.data={};
					}
					//config.data=addTokenInData(config.data);
					//config.data=addCsrfInData(config.data);
					config.data=paramSerializeData(config.data);
				}else{
					if(!angular.isDefined(config.params)){
						config.params={};
					}
					//config.params=addTokenInData(config.params);
				}
			}
            return config;
        },
        response: function (response) {
			if(response.config.url.indexOf(".html")===-1 && response.config.url.indexOf("noloader")===-1){
				//hide_loader();
			}
			if(angular.isObject(response.data)){
				if(angular.isDefined(response.data.NotLogged)){
                    location.href=API_URL+'user/logout';
                }
			}else if(typeof response.data == "string"){
                if(response.data.indexOf('class="login-bx"')!==-1){
                    location.href=API_URL+'user/logout';
                }
            }
			
            return response;
        },
        responseError: function (err) {
            return err;
        }
    };
});


/** Set header type for http post **/
App.config(function ($httpProvider, $sceDelegateProvider){
	//$httpProvider.defaults.withCredentials = true;
    $httpProvider.defaults.headers.post['Content-Type']='application/x-www-form-urlencoded; charset=utf-8';
    $httpProvider.defaults.headers.common["X-Requested-With"] = "XMLHttpRequest";
	$httpProvider.interceptors.push('httpRequestInterceptor');
});

/** Global Functions and Variables **/
App.run(function($rootScope, $sce) {
	$rootScope.SITE_NAME=SITE_NAME;
	
	$rootScope.renderHtml=function (htmlCode) {
		return $sce.trustAsHtml(htmlCode);
	}
	
	$rootScope.ngrange=function (start, end) {
		var a=[];
		for(i=start; i<=end; i++){
			a.push(i);
		}
		return a;
	}
	
	/** Date Format **/
	$rootScope.get_date=function(d, df){
		d=moment(d).format('DD MMM YYYY');
        if(df){
            d=moment(d).format(df);
        }
        return d;
	}
});

/** Pagination Directive **/
App.directive('dirPaging', function () {
	var str='';
    return {
        template: function (elem, attr) {
			var page_ob=typeof attr.pageob != "undefined"?attr.pageob:"page";
			
			str='<ul class="pagination">';
				str+='<li class="page-item {{'+page_ob+'.cur_page<=1?\'disabled\':\'\'}}"><a href="" class="page-link" ng-click="' + attr.fn + '('+page_ob+'.cur_page-1)"> < </a></li>';
            	str+='<li class="page-item {{'+page_ob+'.cur_page==n?\'active\':\'\'}}" ng-repeat="n in ngrange('+page_ob+'.page_start, '+page_ob+'.page_end)"><a href="" class="page-link" ng-click="' + attr.fn + '(n)">{{n}}</a></li>';
				str+='<li class="page-item {{'+page_ob+'.cur_page>='+page_ob+'.total_pages?\'disabled\':\'\'}}"><a href="" class="page-link" ng-click="' + attr.fn + '('+page_ob+'.cur_page+1)"> > </a></li>';
			str+='</ul>';
			return str;
        },
        replace: true,
    };
});

//EOF