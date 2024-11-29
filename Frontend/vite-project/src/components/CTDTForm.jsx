import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";

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
import { addChuongTrinhDaoTao, updateChuongTrinhDaoTao } from "@/api/api-chuongtrinhdaotao";

const formSchema = z.object({
  ten: z.string().min(2, {
    message: "Ten must be at least 2 characters.",
  }),
  nganhId: z.coerce.number({
    message: "nganhID must be a number.",
  }).min(1, {
    message: "nganhID must be greater than 0.",
  }),
});

export function CTDTForm({ chuongTrinhId, handleAdd, handleEdit, setIsDialogOpen }) {
  const form = useForm({
    resolver: zodResolver(formSchema),
    defaultValues: {
      id: chuongTrinhId,
      ten: "",
      nganhId: "",
    },
  });

  async function onSubmit(values) {
    if (chuongTrinhId) {
      const data = await updateChuongTrinhDaoTao(chuongTrinhId, values);
      handleEdit(data);
    } else {
      const data = await addChuongTrinhDaoTao(values);
      handleAdd(data);
      setIsDialogOpen(false);
    }
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        {chuongTrinhId && (
          <FormField
            control={form.control}
            name="id"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Id</FormLabel>
                <FormControl>
                  <Input {...field} readOnly />
                </FormControl>
                <FormDescription>
                  
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
        )}
        <FormField
          control={form.control}
          name="ten"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Tên</FormLabel>
              <FormControl>
                <Input placeholder="Tên chương trình đào tạo (Cử nhân sư phạm) ," {...field} />
              </FormControl>
              <FormDescription>
                Đây là tên của chương trình đào tạo.
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="nganhId"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Ngành Id</FormLabel>
              <FormControl>
                <Input placeholder="1" {...field} />
              </FormControl>
              <FormDescription>
                Đây là mã ngành của chương trình đào tạo.
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button type="submit">Submit</Button>
      </form>
    </Form>
  );
}
