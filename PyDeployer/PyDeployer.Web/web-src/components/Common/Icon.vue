<template>
    <span>    
        <font-awesome-icon :icon="icon" v-bind:class="{'action-icon': action }"></font-awesome-icon>
    </span>
</template>

<script>
import {library} from "@fortawesome/fontawesome-svg-core"
import {fas} from "@fortawesome/free-solid-svg-icons"
import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome"

library.add(fas);

export default {
    name: "app-icon",
    data: function() {
        return { }
    },
    props: {
        icon: {
            type: String,
            required: true
        },
        action: {
            type: Boolean,
            required: false,
            default: false
        }
    },
    methods: {
    
    },
    components: {
        "font-awesome-icon": FontAwesomeIcon
    }
}
</script>

<style scoped>
    .action-icon:hover {
        transform: scale(1.5);
        cursor: pointer;
    }
</style>