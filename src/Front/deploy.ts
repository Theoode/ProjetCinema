import { $ } from "bun";
import { S3Client, PutObjectCommand } from "@aws-sdk/client-s3";
import * as fs from "fs";
import * as path from "path";

const s3 = new S3Client({ region: "eu-west-3" });
const BUCKET_NAME = "scrynbucket";

async function uploadDirectory(directory: string, bucketName: string) {
    const files = fs.readdirSync(directory, { withFileTypes: true });

    for (const file of files) {
        const filePath = path.join(directory, file.name);

        if (file.isDirectory()) {
            await uploadDirectory(filePath, bucketName);
        } else {
            const fileContent = await fs.promises.readFile(filePath);

            await s3.send(new PutObjectCommand({
                Bucket: bucketName,
                Key: filePath.replace("dist/", ""),
                Body: fileContent,
                ContentType: getContentType(file.name),
            }));

            console.log(`Uploaded: ${filePath}`);
        }
    }
}

function getContentType(fileName: string): string {
    const ext = path.extname(fileName).toLowerCase();
    const contentTypes: Record<string, string> = {
        ".html": "text/html",
        ".css": "text/css",
        ".js": "application/javascript",
        ".png": "image/png",
        ".jpg": "image/jpeg",
        ".svg": "image/svg+xml",
    };
    return contentTypes[ext] || "application/octet-stream";
}

async function deploy() {
    await $`bun run build`;
    await uploadDirectory("dist", BUCKET_NAME);
    console.log("🚀 Déploiement terminé avec succès !");
}

deploy().catch(console.error);