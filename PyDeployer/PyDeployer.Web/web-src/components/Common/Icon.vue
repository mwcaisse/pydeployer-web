<template>
    <span>    
        <font-awesome-icon
            :icon="icon"
            :class="{'action-icon': action }"
        />
    </span>
</template>

<script>
import {library} from "@fortawesome/fontawesome-svg-core"
import {fas} from "@fortawesome/free-solid-svg-icons"
import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome"

library.add(fas);

export default {
    name: "AppIcon",
    components: {
        "font-awesome-icon": FontAwesomeIcon
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
    data: function() {
        return { }
    },
    methods: {
    
    }
}
</script>

<style scoped>
    .action-icon:hover {
        transform: scale(1.5);
        cursor: pointer;
    }
</style>