<template>
    <a class="navbar-item" href="/" v-if="!hasSubLinks">
        {{ name }}
    </a>
    <div class="navbar-item has-dropdown is-hoverable" v-else>
        <div class="navbar-link">
            {{ name }}
        </div>
        <div class="navbar-dropdown" :class="{'is-right': right }">
            <a class="navbar-item" v-for="subLink in subLinks" :href="subLink.link">
                {{ subLink.name }}
            </a>      
        </div>
    </div>
</template>

<script>
    import system from "services/System.js"


    export default {
        name: "navigation-bar",
        data: function() {
            return {     
            }
        },
        props: {
            linkId: {
                type: String,
                required: true
            },
            name: {
                type: String,
                required: true
            },
            link: {
                type: String,
                required: false,
                default: function () {
                    return "#";
                }
            },
            subLinks: {
                type: Array,
                required: false,
                default: function () {
                    return [];
                }
            },
            right: {
                type: Boolean,
                default: false
            }
        },
        computed: {
            hasSubLinks: function () {
                return this.subLinks.length > 0;
            }
        },
        methods: {   
        },
        created: function () {
        }
    }
</script>
