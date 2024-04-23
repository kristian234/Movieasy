
import { getCurrentUser } from "@/app/actions/auth-actions";
import { getProfile } from "@/app/actions/profile-actions"
import BackArrow from "@/app/components/shared/back-arrow";
import Link from "next/link";
import { notFound } from "next/navigation";

export default async function DetailsPage({ params }: { params: { id: string } }) {
    const profile = await getProfile(params.id);
    const user = await getCurrentUser();

    if ((profile as any).error) {
        return notFound();
    }

    return (
        <div className="w-full max-w-xl mx-auto bg-body bg-opacity-35 rounded-2xl mt-10 shadow-3xl">
            <div className="pb-1 p-3">
                <div className="flex items-center space-x-4">

                    <div>
                        <BackArrow />
                    </div>
                    <div className="flex items-center space-x-3">
                        <div className="h-16 w-16 border rounded-full overflow-hidden">
                            <img alt="Avatar" className="object-cover bg-white w-full h-full" src="/placeholder-avatar.png" />
                        </div>
                        <div>
                            <div className="text-2xl font-bold text-secondary">{profile.firstName} {profile.lastName}</div>
                            <div className="text-third font-semibold text-xm">Joined in ....</div>
                        </div>
                    </div>
                </div>
            </div>

            <div className="pt-0 space-y-4">
                <div className="">
                    <div className="space-y-2 p-5">
                        <h2 className="text-xl font-semibold text-secondary">About</h2>
                        <p className="text-third font-normal break-words">
                            {profile.details ? profile.details : "No about information"}
                        </p>
                    </div>
                </div>
            </div>

            {user?.userId && user.userId == params.id && (
                <Link href={`/profiles/edit`}>
                    <button className="flex items-center max-w-3xl mt-5 justify-center mx-auto text-secondary py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-semibold w-full bg-header hover:bg-darkHeader focus:outline-none focus:ring-1 focus:ring-secondary">
                        Edit your profile.
                    </button>
                </Link>
            )}
        </div>
    );
}