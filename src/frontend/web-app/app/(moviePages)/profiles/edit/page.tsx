'use server'
import { getCurrentUser } from "@/app/actions/auth-actions";
import { getProfile } from "@/app/actions/profile-actions";
import ProfileForm from "@/app/components/profiles/profile-form";
import { notFound } from "next/navigation";



export default async function ProfileEditPage() {
    const user = await getCurrentUser()
    if (!user?.userId) {
        return notFound();
    }

    const profile = await getProfile(user?.userId);

    return (
        <div>
            <ProfileForm profile={profile} userId={user.userId} />
        </div>
    )
}