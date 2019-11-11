<template>
    <nav class="navbar is-info" role="navigation" aria-label="main navigation">
        <div class="navbar-brand">
            <a class="navbar-item" :href="rootLink">PyDeployer</a>
            <a role="button" class="navbar-burger" aria-label="menu" aria-expanded="false">
                <span aria-hidden="true"></span>
                <span aria-hidden="true"></span>
                <span aria-hidden="true"></span>
            </a>
        </div>
        <div class="navbar-menu">
            <div class="navbar-start">
                <navigation-link v-for="link in navigationLinks"
                                 :key="link.id"
                                 :linkId="link.id"
                                 :name="link.name"
                                 :link="link.link"
                                 :subLinks="link.subLinks"
                >

                </navigation-link>
            </div>
            <div class="navbar-end">
                <navigation-link v-for="link in rightNavigationLinks"
                                 :key="link.id"
                                 :linkId="link.id"
                                 :name="link.name"
                                 :link="link.link"
                                 :subLinks="link.subLinks"
                                 :right="true">
                </navigation-link>
            </div>

        </div>
    </nav>
</template>

<script>  
import {UserService} from "@app/services/ApplicationProxy.js"
import Links from "@app/services/Links.js"
import NavigationLink from "@app/components/Navigation/NavigationLink.vue"

const isAuthenticated = $("#isAuthenticated").val() === "true";

export default {
    name: "navigation-bar",
    data: function() {
        return {     
            navigationLinks: [],
            rightNavigationLinks: [],
            rootLink: Links.home
        }
    },
    methods: {
        initializeLinks: function () {
            if (isAuthenticated) {
                this.fetchCurrentUser().then(function (user) {
                    this.navigationLinks.push({
                        id: "Home", 
                        name: "Home", 
                        link: Links.home
                    })

                    this.navigationLinks.push({
                        id: "build-token", 
                        name: "Build Tokens", 
                        link: Links.buildToken
                    });

                    const userNav = {
                        id: "User", 
                        name: user.name, 
                        link: "", 
                        subLinks: []
                    };

                    userNav.subLinks.push({
                        id: "User/Tokens", 
                        name: "Tokens", 
                        link: Links.userAuthenticationTokens
                    });
                    userNav.subLinks.push({
                        id: "User/Logout", 
                        name: "Logout", 
                        link: Links.logout
                    });

                    this.rightNavigationLinks.push(userNav);
                }.bind(this));
            }                
        },
        fetchCurrentUser: function () {
            return UserService.me().then(function (user) {
                return user;
            })
        }
    },
    components: {
        "navigation-link": NavigationLink
    },
    created: function () {
        this.initializeLinks();
    }

}
</script>
