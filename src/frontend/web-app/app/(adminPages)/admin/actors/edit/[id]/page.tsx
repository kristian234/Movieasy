import { getActor } from "@/app/actions/actor-actions";
import ActorForm from "@/app/components/admin/admin-actors/actor-form";

export default async function ActorEditPage({ params }: { params: { id: string } }) {
    const actor = await getActor(params.id);
    return (
        <div>
            <ActorForm actor={actor} title="Update Actor"></ActorForm>
        </div>
    )
}