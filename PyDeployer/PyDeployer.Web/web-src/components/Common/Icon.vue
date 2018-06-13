<template>
    <span>
        <i class="fas" v-bind:class="[icon, {'action-icon': action }]"></i>
    </span>
</template>

<script>
    //Add all of the fontawesome icons
    //TODO: Figure out if I always want to load the icons
    //      or do it on a module by module basis, for now this works
    import fontawesome from '@fortawesome/fontawesome'
    import solid from "@fortawesome/fontawesome-free-solid"
    fontawesome.library.add(solid);

     export default {
        name: "app-icon",
        data: function() {
            return {
                applications: []
            }
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
    
        }
    }
</script>

<style scoped>
    .action-icon:hover {
        transform: scale(1.5);
        cursor: pointer;
    }
</style>