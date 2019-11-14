<template>
    <a
        v-if="!hasSubLinks"
        class="navbar-item"
        :href="link"
    >
        {{ name }}
    </a>
    <div
        v-else
        class="navbar-item has-dropdown is-hoverable"
    >
        <div class="navbar-link">
            {{ name }}
        </div>
        <div
            class="navbar-dropdown"
            :class="{'is-right': right }"
        >
            <a
                v-for="subLink in subLinks"
                :key="subLink.linkId"
                class="navbar-item"
                :href="subLink.link"
            >
                {{ subLink.name }}
            </a>      
        </div>
    </div>
</template>

<script>
//We import system for its side effects
//eslint-disable-next-line no-unused-vars
import system from "@app/services/System.js"


export default {
    name: "NavigationBar",
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
    data: function() {
        return {     
        }
    },
    computed: {
        hasSubLinks: function () {
            return this.subLinks.length > 0;
        }
    },
    created: function () {
    },
    methods: {   
    }
}
</script>
