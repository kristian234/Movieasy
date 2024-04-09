import ActorForm from "@/app/components/admin/admin-actors/actor-form";

export default async function ActorPage() {
    return(
        <div>
            <h1>ACTOR PAGE</h1>
            <ActorForm title={"Create Actor"} />
        </div>
    )
}