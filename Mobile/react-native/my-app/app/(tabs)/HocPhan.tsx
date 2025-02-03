import React, { useState } from 'react';
import { Alert, FlatList, ScrollView, StyleSheet, Text, TouchableOpacity, View, SafeAreaView, Platform, StatusBar, Image } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { router } from 'expo-router';
import { SelectList } from 'react-native-dropdown-select-list';
import { apiClient } from '../api/apiClient';

const handleFooterPress = (route: string) => {
  if (route === 'home') {
    router.push('/(tabs)/Home');
  } else if (route === 'person') {
    router.push('/(tabs)/Profile');
  }
};

type TableRow = {
  TT: string;
  MaHocPhan: string;
  TenHocPhan: string;
  SoTC: string;
  LaCotLoi: string;
  Khoa: string;
};

type ColumnWidths = {
  [key: string]: number;
};

const Result = () => {
  const [selected, setSelected] = React.useState("");
  const tableHeaders = ['TT', 'Mã học phần', 'Tên', 'Số tín chỉ', 'Là cốt lõi?', 'Khoa'];
  const tableData: TableRow[] = [
    { TT: '1', MaHocPhan: '1021883.2410', TenHocPhan: 'Chuyên đề CN CNPM', SoTC: '2', LaCotLoi: 'false', Khoa: 'CNTT' },
    { TT: '2', MaHocPhan: '1020413.2410', TenHocPhan: 'Kiểm thử phần mềm', SoTC: '2', LaCotLoi: 'false', Khoa: 'CNTT' },
    { TT: '3', MaHocPhan: '1021893.2410', TenHocPhan: 'Kiến trúc hướng dịch vụ', SoTC: '2', LaCotLoi: 'false', Khoa: 'CNTT' },
    { TT: '4', MaHocPhan: '1023780.2410', TenHocPhan: 'PBL6: Dự án công nghệ phần mềm', SoTC: '4', LaCotLoi: 'true', Khoa: 'CNTT' },
    { TT: '5', MaHocPhan: '1024430.2410', TenHocPhan: 'Thực tập chuyên ngành', SoTC: '3', LaCotLoi: 'false', Khoa: 'CNTT' },
  ];
  const semesters = [
    { key: '1', value: 'Học kỳ 1 - Năm 2024-2025' },
    { key: '2', value: 'Học kỳ 2 - Năm 2023-2024' },
    { key: '3', value: 'Học kỳ 1 - Năm 2023-2024' },
  ];
  const handleSelect = () => {
    if (selected) {
      console.log("Selected semester:", selected);
    } else {
      Alert.alert("Thông báo", "Vui lòng chọn học kỳ");
    }
  };
  const columnWidths: ColumnWidths = {
    TT: 40,
    MaHocPhan: 140,
    TenHocPhan: 180,
    SoTC: 90,
    LaCotLoi: 100,
    Khoa: 70,
  };

  const headerToWidthKey: { [key: string]: string } = {
    'TT': 'TT',
    'Mã học phần': 'MaHocPhan',
    'Tên': 'TenHocPhan',
    'Số tín chỉ': 'SoTC',
    'Là cốt lõi?': 'LaCotLoi',
    'Khoa': 'Khoa',
  };

  const renderHeader = () => (
    <View style={[styles.row, styles.headerRow]}>
      {tableHeaders.map((header, index) => (
        <Text
          key={index}
          style={[
            styles.cell,
            styles.headerCell,
            { width: columnWidths[headerToWidthKey[header]] }
          ]}
        >
          {header}
        </Text>
      ))}
    </View>
  );

  const renderRow = ({ item, index }: { item: TableRow; index: number }) => (
    <View style={[styles.row, index % 2 === 0 ? styles.evenRow : styles.oddRow]}>
      <Text style={[styles.cellTT, { width: columnWidths.TT }]}>{item.TT}</Text>
      <Text style={[styles.cell, { width: columnWidths.MaHocPhan }]}>{item.MaHocPhan}</Text>
      <Text style={[styles.cell, { width: columnWidths.TenHocPhan }]}>{item.TenHocPhan}</Text>
      <Text style={[styles.cell, { width: columnWidths.SoTC }]}>{item.SoTC}</Text>
      <Text style={[styles.cell, { width: columnWidths.LaCotLoi }]}>{item.LaCotLoi === 'true' ? 'Có' : 'Không'}</Text>
      <Text style={[styles.cell, { width: columnWidths.Khoa }]}>{item.Khoa}</Text>
    </View>
  );

  return (
    <SafeAreaView style={styles.safeArea}>
      <StatusBar barStyle="light-content" backgroundColor="#0000cc" />
      <View style={styles.container}>
        {/* Header */}
        <View style={styles.header}>
          <View style={styles.headerLeft}>
            <Image
              source={{ uri: 'https://i.imghippo.com/files/mUo4100yA.webp' }}
              style={styles.headerLogo}
            />
          </View>
          <Text style={styles.logoText}>HỌC PHẦN</Text>
          <Ionicons name="notifications-outline" size={24} color="white" />
        </View>

        {/* Add Dropdown Section */}
        <View style={styles.dropdownContainer}>
          <SelectList
            setSelected={setSelected}
            data={semesters}
            placeholder="Chọn học kỳ"
            searchPlaceholder="Tìm học kỳ"
            save="value"
            boxStyles={styles.dropdown}
            dropdownStyles={styles.dropdownList}
          />
          <TouchableOpacity
            style={styles.selectButton}
            onPress={handleSelect}
          >
            <Text style={styles.selectButtonText}>Chọn</Text>
          </TouchableOpacity>
        </View>

        {/* Table Content */}
        <ScrollView horizontal>
          <View style={styles.table}>
            {renderHeader()}
            <FlatList
              data={tableData}
              renderItem={renderRow}
              keyExtractor={(item, index) => index.toString()}
            />
          </View>
        </ScrollView>

        {/* Footer */}
        <View style={styles.footer}>
          {footerItems.map((item, index) => (
            <TouchableOpacity
              key={index}
              style={styles.footerItem}
              onPress={() => handleFooterPress(item.icon)}
            >
              <Ionicons name={item.icon} size={24} color="#0000cc" />
              <Text style={styles.footerText}>{item.label}</Text>
            </TouchableOpacity>
          ))}
        </View>
      </View>
    </SafeAreaView>
  );
};

const footerItems: { icon: 'home' | 'person', label: string }[] = [
  { icon: 'home', label: 'Trang chủ' },
  { icon: 'person', label: 'Hồ sơ' },
];

const styles = StyleSheet.create({
  table: {
    borderWidth: 1,
    borderColor: '#ccc',
    marginHorizontal: 16,
    borderRadius: 8,
    overflow: 'hidden',
    margin: 16,
  },
  row: {
    flexDirection: 'row',
    borderBottomWidth: 1,
    borderBottomColor: '#ccc',
  },
  cellTT: {
    padding: 10,
    textAlign: 'center',
    borderRightWidth: 1,
    borderRightColor: '#ccc',
    justifyContent: 'center',
    alignItems: 'center',
  },
  cell: {
    padding: 10,
    textAlign: 'left',
    borderRightWidth: 1,
    borderRightColor: '#ccc',
    justifyContent: 'center',
  },
  headerRow: {
    backgroundColor: '#f1f1f1',
    borderBottomWidth: 2,
    borderBottomColor: '#ccc',
  },
  headerCell: {
    fontWeight: 'bold',
    fontSize: 14,
    backgroundColor: '#f1f1f1',
    textAlign: 'center',
  },
  evenRow: {
    backgroundColor: '#f9f9f9',
  },
  oddRow: {
    backgroundColor: '#ffffff',
  },
  link: {
    color: 'blue',
    textDecorationLine: 'underline',
    textAlign: 'center',
  },
  safeArea: {
    flex: 1,
    backgroundColor: '#000',
    paddingTop: Platform.OS === 'android' ? StatusBar.currentHeight : 0,
  },
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    backgroundColor: '#0000cc',
    padding: 15,
    height: 70,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  headerLogo: {
    width: 50,
    height: 50,
    marginRight: 10,
  },
  logoText: {
    color: 'white',
    fontSize: 20,
    fontWeight: 'bold',
  },
  footer: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    backgroundColor: 'white',
    paddingVertical: 10,
    borderTopWidth: 1,
    borderTopColor: '#ddd',
  },
  footerItem: {
    alignItems: 'center',
  },
  footerText: {
    marginTop: 5,
    fontSize: 12,
    color: '#0000cc',
  },
  dropdownContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 10,
    gap: 10,
  },
  dropdown: {
    flex: 1,
    borderColor: '#0000cc',
    borderRadius: 8,
    height: 50,
    paddingHorizontal: 12,
  },
  dropdownList: {
    borderColor: '#0000cc',
    marginTop: 5,
    maxHeight: 150,
  },
  selectButton: {
    backgroundColor: '#0000cc',
    paddingHorizontal: 20,
    paddingVertical: 12,
    borderRadius: 8,
    justifyContent: 'center',
    alignItems: 'center',
    height: 43,
  },
  selectButtonText: {
    color: 'white',
    fontWeight: 'bold',
    fontSize: 16,
  },
});

export default Result;