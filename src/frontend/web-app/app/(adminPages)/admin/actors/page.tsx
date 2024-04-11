import ActorForm from "@/app/components/admin/admin-actors/actor-form";
import ActorListing from "@/app/components/admin/admin-actors/actor-listing";

export default async function ActorPage() {
    return(
        <div className="overflow-x-hidden">
            <div className="flex flex-col md:flex-row">

                <div className="md:w-1/2  justify-center">
                    <div className="">
                    <ActorForm title={"Create Actor"} />
                    </div>
                </div>
                <div className="md:w-1/2 justify-center">
                    <div className="">
                        <ActorListing />
                    </div>
                </div>
                
            </div>
        </div>      
    )
}