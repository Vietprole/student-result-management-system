import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { useParams } from "react-router-dom";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { addBaiKiemTra, updateBaiKiemTra } from "@/api/api-baikiemtra";
// import { useNavigate } from "react-router-dom";

const formSchema = z.object({
  loai: z.string().min(2, {
    message: "Loai must be at least 2 characters.",
  }),
  trongSo: z.string()
  .refine((val) => !isNaN(parseFloat(val)), {
    message: "Trong so must be a number",
  })
  .refine((val) => parseFloat(val) > 0 && parseFloat(val) < 1, {
    message: "Trong so must be between 0 and 1",
  }),
  lopHocPhanId: z.coerce.number(
    {
      message: "Lop Hoc Phan Id must be a number",
    }
  ).min(1, {
    message: "Lop Hoc Phan Id must be at least 1 characters.",
  }),
});

export function BaiKiemTraForm({ baiKiemTra, handleAdd, handleEdit, setIsDialogOpen }) {
  const { lopHocPhanId } = useParams();
  // 1. Define your form.
  const form = useForm({
    resolver: zodResolver(formSchema),
    defaultValues: baiKiemTra ||{
      loai: "",
      trongSo: "",
      lopHocPhanId: lopHocPhanId,
    },
  });

  // 2. Define a submit handler.
  async function onSubmit(values) {
    // Do something with the form values.
    // ✅ This will be type-safe and validated.
    if (baiKiemTra) {
      const data = await updateBaiKiemTra(baiKiemTra.id, values);
      handleEdit(data);
    } else {
      const data = await addBaiKiemTra(values);
      handleAdd(data);
      setIsDialogOpen(false);
    }
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        {baiKiemTra && (
          <FormField
            control={form.control}
            name="id"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Id</FormLabel>
                <FormControl>
                  <Input {...field} readOnly/>
                </FormControl>
                <FormDescription>
                  This is your unique identifier.
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
        )}
        <FormField
          control={form.control}
          name="loai"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Loại</FormLabel>
              <FormControl>
                <Input placeholder="Giữa Kỳ" {...field} />
              </FormControl>
              <FormDescription>
                This is your public display name.
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="trongSo"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Trọng Số</FormLabel>
              <FormControl>
                <Input placeholder="0.3" {...field} />
              </FormControl>
              <FormDescription>
                This is your public display name.
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        {/* <FormField
          control={form.control}
          name="lopHocPhanId"
          render={({ field }) => (
            <FormItem>
              <FormLabel>ID Lớp Học Phần</FormLabel>
              <FormControl>
                <Input placeholder="1" {...field} />
              </FormControl>
              <FormDescription>
                This is your public display name.
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        /> */}
        <Button type="submit">Submit</Button>
      </form>
    </Form>
  );
}
